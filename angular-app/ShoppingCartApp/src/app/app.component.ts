import { Component } from '@angular/core';
import {UUID} from 'angular2-uuid';
import { Router } from '@angular/router';
import { UserService } from './Services/UserService';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
})
export class AppComponent {
 
  constructor(private router : Router,private userService : UserService){

  }

  ngOnInit(){
    let user = JSON.parse(localStorage.getItem('user'));

    if(user == null){
      this.router.navigate(['login']);
    }
    else{
      this.router.navigate(['home',user.Id]);
    }
  }

  Logout() {
    alert("Logout");
    setTimeout(() => {
        this.userService.Logout();
        this.router.navigateByUrl('login');
    }, 2000);
}
}
