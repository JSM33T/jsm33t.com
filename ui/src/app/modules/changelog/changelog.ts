import { Component, OnInit } from '@angular/core';
import { HttpApiService } from '../../services/httpapi-service';
import { environment } from '../../../environments/environment';
import { ModalService } from '../../services/modal-service';
import { ApiResponse } from '../../interfaces/IApiResponse';
import { CommonModule } from '@angular/common';

export type ChangeType = 'Added' | 'Removed' | 'Fixed';

export interface MdlChangeLog {
	id: number;
	version: string;
	type: ChangeType;
	description: string;
	createdAt: string;
}

interface ChangeLogGroup {
	version: string;
	items: MdlChangeLog[];
}

@Component({
	selector: 'app-changelog',
	imports: [CommonModule],
	templateUrl: './changelog.html',
	styleUrl: './changelog.css'
})
export class Changelog implements OnInit {

	changelog: MdlChangeLog[] = [];
	changelogGrouped: ChangeLogGroup[] = [];
	apiUrl: string = environment.apiUrl;

	constructor(private httpApiService: HttpApiService, private modalService: ModalService) { }

	ngOnInit(): void {
		this.httpApiService.get<MdlChangeLog[]>(`${this.apiUrl}/changelog`).subscribe(res => {
			this.changelog = res.data;
			this.groupByVersion();
		});

		// setTimeout(() => {
		// 	this.modalService.open('customizer-modal', 'My Title', 'This is body message', ['Hint 1', 'Hint 2']);
		// 	console.log('Modal opened');
		// }, 1000);
	}

	groupByVersion(): void {
		const grouped: { [version: string]: MdlChangeLog[] } = {};
		this.changelog.forEach(log => {
			if (!grouped[log.version]) {
				grouped[log.version] = [];
			}
			grouped[log.version].push(log);
		});
		this.changelogGrouped = Object.keys(grouped).map(version => ({
			version,
			items: grouped[version]
		}));
	}
}
