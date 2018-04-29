import { Injectable } from '@angular/core';
import { SessaoUsuario } from '../data/dataModels';
import { Router } from '@angular/router';
import { ApiService } from './api.service';

@Injectable()
export class AuthService {

  private _sessaoUser:SessaoUsuario = new SessaoUsuario();

    constructor(
    private router: Router,
    private servidor: ApiService
  )
  {}

  public logout(option:number = 0) : void {
    switch(option){
      case 1:
          if(confirm("Deseja realmente sair?")){
            localStorage.removeItem('access_token');
            this.router.navigate(['/Login']);
          }
      break;
      default:
        localStorage.removeItem('access_token');
        this.router.navigate(['/Login']);
    }
  }

  public authenticate(update:boolean = false) : Promise<SessaoUsuario>{
    return new Promise<SessaoUsuario>(async resolve =>{
        let token: string = localStorage.getItem("access_token");
        if(token){
          if((this._sessaoUser.Token != token) || update){
            let sec:SessaoUsuario = await this.servidor.chamarApi('api/acesso/sessoes',null);
            this._sessaoUser = sec;
          }
        }
        resolve(this._sessaoUser);
    });
  }

  public getRidUser() : Promise<string>{
    return new Promise<string>(async resolve =>{
      let session = await this.authenticate();
      if(session)
        return session.UserId;
      else
        return null;  
    });
  }

}
