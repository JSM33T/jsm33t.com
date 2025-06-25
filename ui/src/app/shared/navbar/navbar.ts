import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

/**
 * NavbarComponent - Handles the navigation bar UI including:
 * - Responsive menu toggle
 * - Theme switching (light/dark)
 * - Route navigation
 */
@Component({
	selector: 'app-navbar',
	standalone: true,
	imports: [CommonModule, RouterModule, FormsModule],
	templateUrl: './navbar.html',
	styleUrls: ['./navbar.css']
})
export class Navbar implements OnInit {

	/** Indicates whether the mobile navbar is open */
	isNavbarOpen = false;

	/** Controls the theme toggle state (dark/light) */
	isDarkMode = false;

	/**
	 * Initializes component state based on localStorage theme
	 */
	ngOnInit(): void {
		const storedTheme = localStorage.getItem('theme') || 'light';
		this.isDarkMode = storedTheme === 'dark';
		this.applyTheme(storedTheme);
	}

	/**
	 * Toggles the mobile navbar open/close
	 */
	toggleNavbar(): void {
		this.isNavbarOpen = !this.isNavbarOpen;
	}

	/**
	 * Closes the mobile navbar (used on nav item click)
	 */
	closeNavbar(): void {
		this.isNavbarOpen = false;
	}

	/**
	 * Handles theme toggle change and updates localStorage + DOM
	 */
	onThemeToggle(): void {
		const theme = this.isDarkMode ? 'dark' : 'light';
		localStorage.setItem('theme', theme);
		this.applyTheme(theme);
	}

	/**
	 * Applies the given theme to the <html> element via data-bs-theme
	 * @param theme 'light' | 'dark'
	 */
	private applyTheme(theme: string): void {
		document.documentElement.setAttribute('data-bs-theme', theme);
	}
}
