import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../_models/user';
import { AccountService } from '../_services/account.service';
import{of} from 'rxjs';
@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
model:any={};

currentUsers$:Observable<User | null>=null;
  constructor(public accountService:AccountService) 
  { 


  }

  ngOnInit(): void {
    this.currentUsers$=this.accountService.currentUsers$;
  }
  
  login()
  {
    this.accountService.login(this.model).subscribe(response=>{
    console.log(response);

},
error=>
{ console.log(error);
 })
  }
  logout(){
    this.accountService.logout();

  }
}
