import { Component, OnInit } from '@angular/core';
import { Profile } from '../../profile/profile-edit/Profile.model';

@Component({
  selector: 'app-profiles-list',
  templateUrl: './profiles-list.component.html',
  styleUrls: ['./profiles-list.component.scss']
})
export class ProfilesListComponent implements OnInit {


  profiles: Profile[] = [
     {
      ProfileId: 0,
      FirstName: 'Nicholas',
      Surname: 'Namacha',
      DateOfBirth: new Date(2017, 4, 4, 17, 23, 42, 11),
      IdentificationTypes: 1,
      IdentificationNumber: '123456',
      MobileNumber: '0846994704',
      Email: 'user@example.com',
      NextOfKin: 0,
      ProfileTypes: 0,
      Statuses: 0,
      ActivationDate: new Date(2017, 4, 4, 17, 23, 42, 11),
      Address: '15 test road',
      Image: '../../assets/michael-dam-mEZ3PoFGs_k-unsplash.jpg',
      CreatedBy: 0,
      Created: new Date(2017, 4, 4, 17, 23, 42, 11),
      UpdatedBy: 0,
      ApprovedBy: 0,
      Approved: new Date(2017, 4, 4, 17, 23, 42, 11)

    },
    {
      ProfileId: 1,
      FirstName: 'Marvelous',
      Surname: 'Namacha',
      DateOfBirth: new Date(2017, 4, 4, 17, 23, 42, 11),
      IdentificationTypes: 1,
      IdentificationNumber: '123456',
      MobileNumber: '0846994704',
      Email: 'user@example.com',
      NextOfKin: 0,
      ProfileTypes: 0,
      Statuses: 0,
      ActivationDate: new Date(2017, 4, 4, 17, 23, 42, 11),
      Address: '15 test road',
      Image: '../../assets/michael-dam-mEZ3PoFGs_k-unsplash.jpg',
      CreatedBy: 0,
      Created: new Date(2017, 4, 4, 17, 23, 42, 11),
      UpdatedBy: 0,
      ApprovedBy: 0,
      Approved: new Date(2017, 4, 4, 17, 23, 42, 11)

    }


  ];
  constructor() { }

  ngOnInit() {
  }




}
