import { Component, OnInit, Inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';


@Component({
  selector: 'app-channel-detail',
  templateUrl: './channel-detail.component.html',
  styleUrls: ['./channel-detail.component.css']
})
export class ChannelDetailComponent implements OnInit {

  constructor(
    private route: ActivatedRoute,
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string
  ) { }

  channelId: string;
  channel: ChannelDetail;

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      this.channelId = params.get('channelId');
      this.http.get<ChannelDetail>(`${this.baseUrl}api/channels/${this.channelId}`).subscribe(result => {
        this.channel = result;
      }, error => console.error(error));
    });
  }
}

interface ChannelDetail {
  id: string;
  title: string;
  description: string;
  commentCount: number;
  subscriberCount: number;
  videoCount: number;
}

