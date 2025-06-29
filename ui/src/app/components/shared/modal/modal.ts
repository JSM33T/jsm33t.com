import { Component } from '@angular/core';
import { ModalService } from '../../../services/modal-service';

@Component({
	selector: 'app-modal',
	imports: [],
	templateUrl: './modal.html',
	styleUrl: './modal.css'
})
export class Modal {

	constructor(public modalService: ModalService) { }

}
