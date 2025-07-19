import { HttpClient } from '@angular/common/http';
import { Component, signal, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.html',
  styleUrl: './app.css'
})

export class App implements OnInit{
  private http = inject(HttpClient);
  protected title = signal('Dating App'); //signal is used to create a reactive variable
  protected members = signal<any>([])

  ngOnInit(): void {
    this.http.get("http://localhost:5001/api/members").subscribe({
      next: response => this.members.set(response), // Update the members signal with the response from the API
      // next: response => this.members.set(response), // Alternatively, you can use set to
      // Handle the response from the API
      error: error => console.error(error),
      complete: () => console.log('Request completed')
    })
    }
}


