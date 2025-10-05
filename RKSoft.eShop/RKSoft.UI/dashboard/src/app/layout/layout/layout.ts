import { Component } from "@angular/core";
import { Header } from "../header/header";
import { RouterOutlet } from "@angular/router";
import { Sidebar } from "../sidebar/sidebar";
import { Footer } from "../footer/footer";

@Component({
  selector: "app-layout",
  imports: [Header, RouterOutlet, Sidebar, Footer],
  templateUrl: "./layout.html",
  styleUrl: "./layout.css",
})
export class Layout {
  isSidebarCollapsed = false;

  onToggleSidebar(collapsed: boolean) {
    this.isSidebarCollapsed = collapsed;
  }
}
