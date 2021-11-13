import { Component, Input, OnInit } from '@angular/core';
import { Profile } from 'src/app/profile/profile-edit/Profile.model';

@Component({
  selector: 'app-profile-detail',
  templateUrl: './profile-detail.component.html',
  styleUrls: ['./profile-detail.component.scss']
})
export class ProfileDetailComponent implements OnInit {

  constructor() { }
  @Input() currentProfile: Profile;
  ngOnInit() {
  }

}
