import { Component, EventEmitter, Output} from '@angular/core';

@Component({
  selector: 'app-sidebar',
  imports: [],
  templateUrl: './sidebar.html',
  styleUrl: './sidebar.css'
})
export class Sidebar {
isSidebarCollapsed = false;
  @Output() toggle = new EventEmitter<boolean>();

  toggleSidebar() {
    this.isSidebarCollapsed = !this.isSidebarCollapsed;
    this.toggle.emit(this.isSidebarCollapsed);
  }
}
