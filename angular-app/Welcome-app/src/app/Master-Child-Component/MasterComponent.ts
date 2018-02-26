import { Component } from '@angular/core';
  
  
  @Component({
      selector :'ht-master',
      templateUrl : 'MasterComponent.html'
  })
  export class MasterComponent{

    stateChangeHandler(e){
        console.log(e);
    }

    clickRatingHandler(e){
        console.log("Rating is: "+e);
    }
  }