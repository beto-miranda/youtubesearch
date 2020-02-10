import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-channels-search',
  templateUrl: './channels-search.component.html',
  styleUrls: ['./channels-search.component.css']
})
export class ChannelsSearchComponent implements OnInit {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    this.list();
    this.searchTermUpdate.pipe(
      debounceTime(400),
      distinctUntilChanged())
      .subscribe(value => {
        this.list();
      });
  }

  public channels: Channel[];
  public searchTerm = '';
  public fromCache = false;
  searchTermUpdate = new Subject<string>();

  ngOnInit(): void {

  }

  list() {
    this.http.get<Channel[]>(`${this.baseUrl}api/channels?searchTerm=${this.searchTerm}&fromCache=${this.fromCache}`).subscribe(result => {
      this.channels = result;
    }, error => console.error(error));
  }
}

interface Channel {
  id: string;
  title: string;
  description: string;
}


