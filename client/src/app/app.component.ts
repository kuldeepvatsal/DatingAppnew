import { JsonPipe } from '@angular/common';
import { templateJitUrl } from '@angular/compiler';
import { Component, OnInit } from '@angular/core';

import { User } from './_models/user';
import { AccountService } from './_services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'The Dating App';

  
  constructor(private accountService:AccountService){}
  ngOnInit(): void {
   // this.getUsers();
    this.setCurrentUser();
  }

 
setCurrentUser()
{
  const user:User =JSON.parse(localStorage.getItem('user'))
if(!user.username)return;
this.accountService.setCurrentUser(user);
console.log(user);
}

}
