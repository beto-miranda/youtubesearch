import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { VideosSearchComponent } from './videos-search/videos-search.component';
import { ChannelsSearchComponent } from './channels-search/channels-search.component';
import { VideoDetailComponent } from './video-detail/video-detail.component';
import { ChannelDetailComponent } from './channel-detail/channel-detail.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    VideosSearchComponent,
    ChannelsSearchComponent,
    VideoDetailComponent,
    ChannelDetailComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'videos', component: VideosSearchComponent },
      { path: 'videos/:videoId', component: VideoDetailComponent },
      { path: 'channels', component: ChannelsSearchComponent },
      { path: 'channels/:channelId', component: ChannelDetailComponent },
    ])
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
