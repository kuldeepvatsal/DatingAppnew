import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../_models/user';
import { AccountService } from '../_services/account.service';
import{of} from 'rxjs';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr/public_api';
@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
model:any={};

currentUsers$:Observable<User | null>=null;
  constructor(public accountService:AccountService,private router: Router,private toastr:ToastrService) 
  { 


  }

  ngOnInit(): void {
    this.currentUsers$=this.accountService.currentUsers$;
  }
  
  login()
  {
     this.accountService.login(this.model).subscribe(_=>
     this.router.navigateByUrl('/members'),
 error=>
 { 
  this.toastr.error(error)
 })
  }
  logout(){
    this.accountService.logout();
    this.router.navigateByUrl('/');

  }
}
