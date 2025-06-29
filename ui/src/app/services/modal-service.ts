import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment'; // adjust path if needed

@Injectable({
	providedIn: 'root'
})
export class ModalService {
	isModalOpen = false;

	constructor() { }

	open(id: string, title: string, message: string, hints: string[] = []): void {
		const modal = document.getElementById(id);
		if (!modal) return;

		this.isModalOpen = true;

		if (environment.verboseLogging) {
			console.log(`Modal opened: ${id}`);
		}

		// Set title and message
		const titleElem = modal.querySelector('.modal-title');
		const bodyElem = modal.querySelector('.modal-body');

		if (titleElem) titleElem.textContent = title;
		if (bodyElem) {
			bodyElem.innerHTML = `<p>${message}</p>`;
			hints.forEach(h => {
				bodyElem.innerHTML += `<p class="hint">${h}</p>`;
			});
		}

		// Add backdrop
		const backdrop = document.createElement('div');
		backdrop.className = 'modal-backdrop fade';
		backdrop.id = `backdrop-${id}`;
		document.body.appendChild(backdrop);

		setTimeout(() => backdrop.classList.add('show'), 10);

		// Show modal with fade
		modal.classList.add('show');
		modal.style.display = 'block';
	}

	close(id: string): void {
		this.isModalOpen = false;
		const modal = document.getElementById(id);
		const backdrop = document.getElementById(`backdrop-${id}`);
		if (!modal) return;

		modal.classList.remove('show');
		modal.style.display = 'none';

		// Remove backdrop with fade
		if (backdrop) {
			backdrop.classList.remove('show');
			setTimeout(() => backdrop.remove(), 150);
		}

		if (environment.verboseLogging) {
			console.log(`Modal closed: ${id}`);
			const openModals = Array.from(document.querySelectorAll('.modal.show')).map(m => m.id);
			console.log(`Currently open modals: ${openModals.join(', ') || 'none'}`);
		}
	}

	closeAll(): void {
		const modals = document.querySelectorAll('.modal.show');
		modals.forEach(m => {
			const id = m.id;
			this.close(id);
		});

		if (environment.verboseLogging) {
			const closedIds = Array.from(modals).map(m => m.id);
			console.log(`All modals closed: ${closedIds.join(', ') || 'none'}`);
		}

		this.isModalOpen = false;
	}
}
