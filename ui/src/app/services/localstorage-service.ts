import { Injectable } from '@angular/core';

@Injectable({
	providedIn: 'root',
})
export class LocalStorageService {

	constructor() { }

	// Get data from local storage by key
	get<T>(key: string): T | null {
		const stored = localStorage.getItem(key);
		return stored ? JSON.parse(stored) : null;
	}

	// Set data to local storage
	set<T>(key: string, value: T): void {
		localStorage.setItem(key, JSON.stringify(value));
	}

	// Remove a key from local storage
	remove(key: string): void {
		localStorage.removeItem(key);
	}

	// Clear all data in local storage
	clear(): void {
		localStorage.clear();
	}
}
