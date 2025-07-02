import { Injectable } from '@angular/core';
import { Renderer2, Inject, Injector } from '@angular/core';
import { DOCUMENT } from '@angular/common';

@Injectable({
	providedIn: 'root',
})
export class OffcanvasService {

	constructor(@Inject(DOCUMENT) private document: Document) { }

	// Function to open the offcanvas using its ID
	openOffcanvas(offcanvasId: string): void {
		const offcanvasElement = this.document.getElementById(offcanvasId);
		if (offcanvasElement) {
			// Manually trigger the show action
			offcanvasElement.classList.add('show');
			offcanvasElement.style.visibility = 'visible';
			offcanvasElement.style.opacity = '1';
		} else {
			console.error('Offcanvas with ID ' + offcanvasId + ' not found');
		}
	}

	// Function to close the offcanvas using its ID
	closeOffcanvas(offcanvasId: string): void {
		const offcanvasElement = this.document.getElementById(offcanvasId);
		if (offcanvasElement) {
			// Manually trigger the hide action
			offcanvasElement.classList.remove('show');
			offcanvasElement.style.visibility = 'hidden';
			offcanvasElement.style.opacity = '0';
		} else {
			console.error('Offcanvas with ID ' + offcanvasId + ' not found');
		}
	}


	// Function to toggle the offcanvas using its ID
	toggleOffcanvas(offcanvasId: string): void {
		const offcanvasElement = this.document.getElementById(offcanvasId);
		if (offcanvasElement) {
			if (offcanvasElement.classList.contains('show')) {
				this.closeOffcanvas(offcanvasId);
			} else {
				this.openOffcanvas(offcanvasId);
			}
		} else {
			console.error('Offcanvas with ID ' + offcanvasId + ' not found');
		}
	}

	// Function to get status if offcanvas is open or closed
	isOffcanvasOpen(offcanvasId: string): boolean {
		const offcanvasElement = this.document.getElementById(offcanvasId);
		if (offcanvasElement) {
			return offcanvasElement.classList.contains('show');
		} else {
			console.error('Offcanvas with ID ' + offcanvasId + ' not found');
			return false;
		}
	}
}
