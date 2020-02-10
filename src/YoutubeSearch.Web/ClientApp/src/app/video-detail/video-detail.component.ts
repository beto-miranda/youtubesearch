import { Component, OnInit, Inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-video-detail',
  templateUrl: './video-detail.component.html',
  styleUrls: ['./video-detail.component.css']
})
export class VideoDetailComponent implements OnInit {

  constructor(
    private route: ActivatedRoute,
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string
  ) { }

  videoId: string;
  video: VideoDetail;

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      this.videoId = params.get('videoId');
      this.http.get<VideoDetail>(`${this.baseUrl}api/videos/${this.videoId}`).subscribe(result => {
        this.video = result;
      }, error => console.error(error));
    });
  }
}

interface VideoDetail {
  id: string;
  title: string;
  description: string;
  commentCount: number;
  likeCount: number;
  dislikeCount: number;
}
