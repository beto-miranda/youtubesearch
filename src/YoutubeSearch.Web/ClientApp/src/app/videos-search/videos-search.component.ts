import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';

@Component({
  selector: 'app-videos-search',
  templateUrl: './videos-search.component.html',
  styleUrls: ['./videos-search.component.css']
})
export class VideosSearchComponent implements OnInit {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    this.list();
    this.searchTermUpdate.pipe(
      debounceTime(400),
      distinctUntilChanged())
      .subscribe(value => {
        this.list();
      });
  }

  public videos: Video[];
  public searchTerm = '';
  public fromCache = false;
  searchTermUpdate = new Subject<string>();

  ngOnInit(): void {

  }

  list() {
    this.http.get<Video[]>(`${this.baseUrl}api/videos?searchTerm=${this.searchTerm}&fromCache=${this.fromCache}`).subscribe(result => {
      this.videos = result;
    }, error => console.error(error));
  }
}

interface Video {
  id: string;
  title: string;
  description: string;
}

