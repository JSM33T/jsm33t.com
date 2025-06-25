import { Component, OnInit, inject } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms'; // Importing reactive forms in Angular 20
import { Router } from '@angular/router';
import { OffcanvasService } from '../../../services/offcanvas-service';
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-chatbot',
  imports: [ReactiveFormsModule,CommonModule],
  templateUrl: './chatbot.html',
  styleUrls: ['./chatbot.css']
})
export class Chatbot implements OnInit {
  // Declare form group and controls
  messageForm: FormGroup;
  messages: { from: 'user' | 'bot'; text: string }[] = [];
  isLoading: boolean = false;

  // Initialize necessary services
  offcanvasService = inject(OffcanvasService);
  router = inject(Router);

  constructor() {
    // Define the reactive form controls
    this.messageForm = new FormGroup({
      userInput: new FormControl('', [Validators.required]) // userInput control with required validation
    });
  }

  ngOnInit(): void {
    // Check for saved messages in localStorage or initialize with default message
    const savedMessages = localStorage.getItem('chat_history');
    if (savedMessages) {
      this.messages = JSON.parse(savedMessages);
    } else {
      this.messages = [
        { from: 'bot', text: 'Hello! I am Kitty Bot. How can I assist you today?' }
      ];
    }
    // Store messages in localStorage for persistence
    localStorage.setItem('chat_history', JSON.stringify(this.messages));
  }

  // Open the chatbot offcanvas
  openChatbot(): void {
    this.offcanvasService.openOffcanvas('chatPanel');
  }

  // Close the chatbot offcanvas
  closeChatbot(): void {
    this.offcanvasService.closeOffcanvas('chatPanel');
  }

  // Handle form submission
  onSubmit(): void {
    const userMessage = this.messageForm.value.userInput?.trim();
    if (userMessage) {
      this.messages.push({ from: 'user', text: userMessage });
      this.isLoading = true;

      // Simulate a bot response
      setTimeout(() => {
        this.messages.push({ from: 'bot', text: `Bot response: ${userMessage}` });
        this.isLoading = false;
        this.messageForm.reset(); // Reset the form after submission
        this.scrollToBottom(); // Scroll to the bottom of the chat
        localStorage.setItem('chat_history', JSON.stringify(this.messages)); // Save chat history
      }, 1000);
    }
  }

  // Scroll the chat to the bottom
  scrollToBottom(): void {
    const scrollContainer = document.getElementById('scrollContainer');
    if (scrollContainer) {
      scrollContainer.scrollTop = scrollContainer.scrollHeight;
    }
  }

  // Handle Enter key for message submission
  handleKeyDown(event: KeyboardEvent): void {
    if (event.ctrlKey && event.key === 'Enter') {
      this.onSubmit();
      event.preventDefault();
    }
  }
}
