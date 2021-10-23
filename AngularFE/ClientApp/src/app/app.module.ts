import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { ProfilesComponent } from './profiles/profiles.component';
import { FuneralComponent } from './funeral/funeral.component';
import { FuneralListComponent } from './funeral/funeral-list/funeral-list.component';
import { FuneralDetailsComponent } from './funeral/funeral-list/funeral-details/funeral-details.component';
import { FuneralEditComponent } from './funeral/funeral-edit/funeral-edit.component';
import { ProfileEditComponent } from './profile/profile-edit/profile-edit.component';
import { ProfilesListComponent } from './profiles/profiles-list/profiles-list.component';
import { ProfileDetailComponent } from './profiles/profiles-list/profile-detail/profile-detail.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    ProfilesComponent,
    FuneralComponent,
    FuneralListComponent,
    FuneralDetailsComponent,
    FuneralEditComponent,
    ProfileEditComponent,
    ProfilesListComponent,
    ProfileDetailComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' }

    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
