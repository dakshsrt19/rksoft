import { Component, OnInit } from "@angular/core";
import { StoreItem, Storeservice } from "./services/storeservice";
import { CommonModule } from "@angular/common";
import { FormsModule, NgForm } from "@angular/forms";

@Component({
  selector: "app-stores",
  imports: [CommonModule, FormsModule],
  templateUrl: "./stores.html",
  styleUrls: ["./stores.scss"],
})
export class Stores implements OnInit {
  stores: StoreItem[] = [];
  searchTerm: string = "";
  currentPage: number = 1;
  pageSize: number = 10; // change as needed

  sortKey: keyof StoreItem | "" = ""; // e.g., 'storeName', 'description'
  sortDirection: "asc" | "desc" = "asc";

  store: Partial<StoreItem> = {
    storeName: "",
    description: ""
  };

  isEditMode: boolean = false;
  constructor(private storeService: Storeservice) {}

  ngOnInit(): void {
    this.getAllItems();
  }

  get filteredStores(): StoreItem[] {
    const term = this.searchTerm?.toLowerCase() || "";

    return this.stores.filter(
      (item) =>
        item.storeName?.toLowerCase().includes(term) ||
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

  getAllItems(): void {
    this.storeService.getAllStores().subscribe((response: any) => {
      if (Array.isArray(response)) {
        this.stores = response;
      } else if (Array.isArray(response.data)) {
        this.stores = response.data;
      } else {
        console.error("Unexpected response format:", response);
        this.stores = [];
      }
      console.log("api data", this.stores);
    });
  }
  onSubmit(form: NgForm) {
    if (!form.valid) return;
    if (this.isEditMode && this.store.id) {
      // 🔄 Update
      this.storeService
        .updateStore(this.store.id, this.store as StoreItem)
        .subscribe(() => {
          this.getAllItems();
          this.resetForm(form);
        });
    }
    if (form.valid) {
      this.store.isActive = true;
      this.store.isDeleted = false;
      this.storeService.createStore(this.store as StoreItem).subscribe({
        next: (response) => {
          this.getAllItems();
          form.resetForm(); // clear form
        },
        error: (err) => {
          console.error("Failed to save store:", err);
        },
      });
    }
  }

  editItem(item: StoreItem): void {
    this.store = { ...item }; // Populate form
    this.isEditMode = true;
  }

  deleteItem(item: StoreItem): void {
    const updatedItem: StoreItem = {
      ...item,
      isActive: false,
      isDeleted: true,
    };
    this.storeService.updateStore(item.id, updatedItem).subscribe(() => {
      this.getAllItems(); // refresh list
    });
  }

  activateItem(item: StoreItem): void {
    const updatedItem: StoreItem = {
      ...item,
      isActive: true,
      isDeleted: false,
    };
    this.storeService.updateStore(item.id, updatedItem).subscribe(() => {
      this.getAllItems(); // refresh list
    });
  }

  // deleteItem(id: number): void {
  //   this.storeService.deleteStore(id).subscribe(() => {
  //     this.getAllItems();
  //   });
  // }

  resetForm(form: NgForm): void {
    form.resetForm();
    this.store = { storeName: "", description: "" };
    this.isEditMode = false;
  }

  get sortedStores(): StoreItem[] {
    const sorted = [...this.filteredStores];

    if (this.sortKey) {
      sorted.sort((a, b) => {
        const valA = a[this.sortKey as keyof StoreItem];
        const valB = b[this.sortKey as keyof StoreItem];

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

  get paginatedStores(): StoreItem[] {
    const startIndex = (this.currentPage - 1) * this.pageSize;
    return this.sortedStores.slice(startIndex, startIndex + this.pageSize);
  }

  get totalPages(): number {
    return Math.ceil(this.filteredStores.length / this.pageSize);
  }

  changePage(page: number): void {
    if (page >= 1 && page <= this.totalPages) {
      this.currentPage = page;
    }
  }

  setSort(key: keyof StoreItem): void {
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
