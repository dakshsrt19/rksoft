import { Component, OnInit } from '@angular/core';
import { CategoryItem, CategoryService } from '../../services/category.service';
import { FormsModule, NgForm } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Storeservice } from '../../services/storeservice';

@Component({
  selector: 'app-category',
  imports: [CommonModule, FormsModule],
  templateUrl: "./category.html",
  styleUrls: ["./category.scss"]
})
export class Category implements OnInit {
  _categories: CategoryItem[] = [];
  searchTerm: string = "";
  currentPage: number = 1;
  pageSize: number = 10; // change as needed
  isActive: boolean = true;
  _stores: any[] =[];

  sortKey: keyof CategoryItem | "" = ""; // e.g., 'CategoryName', 'description'
  sortDirection: "asc" | "desc" = "asc";

  _category: Partial<CategoryItem> = {
    categoryName: "",
    description: ""
  };

  isEditMode: boolean = false;
  constructor(private _categoryService: CategoryService, private _storeService:Storeservice) {}

  ngOnInit(): void {
    this.getAllStoress();
    this.getAllItems();
  }

  get filteredCategorys(): CategoryItem[] {
    const term = this.searchTerm?.toLowerCase() || "";

    return this._categories.filter(
      (item) =>
        item.categoryName?.toLowerCase().includes(term) ||
        item.description?.toLowerCase().includes(term) ||
        item.createdAt?.toString().toLowerCase().includes(term) ||
        (item.isActive
          ? "active"
          : item.isDeleted
          ? "blocked"
          : "inactive"
        ).includes(term)
    );
  }

getAllStoress(): void {
    this._storeService.getAllActiveStores(this.isActive).subscribe((response: any) => {
      if (Array.isArray(response)) {
        this._stores = response;
      } else if (Array.isArray(response.data)) {
        this._stores = response.data;
      } else {
        console.error("Unexpected response format:", response);
        this._stores = [];
      }
      console.log("api data", this._stores);
    });
  }
  getAllItems(): void {
    this._categoryService.getAllcategorys().subscribe((response: any) => {
      if (Array.isArray(response)) {
        this._categories = response;
      } else if (Array.isArray(response.data)) {
        this._categories = response.data;
      } else {
        console.error("Unexpected response format:", response);
        this._categories = [];
      }
      console.log("api data", this._categories);
    });
  }
  onSubmit(form: NgForm) {
    if (!form.valid) return;
    if (this.isEditMode && this._category.id) {
      // 🔄 Update
      this._categoryService
        .updatecategory(this._category.id, this._category as CategoryItem)
        .subscribe(() => {
          this.getAllItems();
          this.resetForm(form);
        });
    }
    if (form.valid) {
      this._category.isActive = true;
      this._category.isDeleted = false;
      this._categoryService.createcategory(this._category as CategoryItem).subscribe({
        next: (response) => {
          this.getAllItems();
          form.resetForm(); // clear form
        },
        error: (err) => {
          console.error("Failed to save Category:", err);
        },
      });
    }
  }

  editItem(item: CategoryItem): void {
    this._category = { ...item }; // Populate form
    this.isEditMode = true;
  }

  deleteItem(item: CategoryItem): void {
    const updatedItem: CategoryItem = {
      ...item,
      isActive: false,
      isDeleted: true,
    };
    this._categoryService.updatecategory(item.id, updatedItem).subscribe(() => {
      this.getAllItems(); // refresh list
    });
  }

  activateItem(item: CategoryItem): void {
    const updatedItem: CategoryItem = {
      ...item,
      isActive: true,
      isDeleted: false,
    };
    this._categoryService.updatecategory(item.id, updatedItem).subscribe(() => {
      this.getAllItems(); // refresh list
    });
  }

  // deleteItem(id: number): void {
  //   this._categoryService.deleteCategory(id).subscribe(() => {
  //     this.getAllItems();
  //   });
  // }

  resetForm(form: NgForm): void {
    form.resetForm();
    this._category = { categoryName: "", description: "" };
    this.isEditMode = false;
  }

  get sortedCategorys(): CategoryItem[] {
    const sorted = [...this.filteredCategorys];

    if (this.sortKey) {
      sorted.sort((a, b) => {
        const valA = a[this.sortKey as keyof CategoryItem];
        const valB = b[this.sortKey as keyof CategoryItem];

        // Handle string and number comparison
        if (typeof valA === "string" && typeof valB === "string") {
          return this.sortDirection === "asc"
            ? valA.localeCompare(valB)
            : valB.localeCompare(valA);
        }

        if (typeof valA === "number" && typeof valB === "number") {
          return this.sortDirection === "asc" ? valA - valB : valB - valA;
        }

        // Optional: handle date comparisons if needed
        if (valA instanceof Date && valB instanceof Date) {
          return this.sortDirection === "asc"
            ? valA.getTime() - valB.getTime()
            : valB.getTime() - valA.getTime();
        }

        return 0;
      });
    }

    return sorted;
  }

  get paginatedCategorys(): CategoryItem[] {
    const startIndex = (this.currentPage - 1) * this.pageSize;
    return this.sortedCategorys.slice(startIndex, startIndex + this.pageSize);
  }

  get totalPages(): number {
    return Math.ceil(this.filteredCategorys.length / this.pageSize);
  }

  changePage(page: number): void {
    if (page >= 1 && page <= this.totalPages) {
      this.currentPage = page;
    }
  }

  setSort(key: keyof CategoryItem): void {
    if (this.sortKey === key) {
      // toggle direction
      this.sortDirection = this.sortDirection === "asc" ? "desc" : "asc";
    } else {
      this.sortKey = key;
      this.sortDirection = "asc";
    }
    this.currentPage = 1; // Reset to first page
  }
}

