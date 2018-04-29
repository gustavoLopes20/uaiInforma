import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
    
  public active:Boolean = false;
  public itens:Array<any>;

  constructor (){}

  ngOnInit() {
   
  }


  //menu
  open(c:boolean){
    if(c){
      this.active = true;
    }else{
      this.active = false;
    }
    
  }


}
