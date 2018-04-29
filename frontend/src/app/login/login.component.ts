import { Component, OnInit } from '@angular/core';
import { Router } from "@angular/router";
import { ApiService } from '../services/api.service';
import { LoginResponseModel } from '../data/dataModels';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  public carregando: boolean = false;
  public formulario:FormGroup;

  constructor(
    private servidor:ApiService,
    private router:Router,
    private formBuilder:FormBuilder,
  ) { }

  ngOnInit() {
    this.formulario = this.formBuilder.group({
      Email: [null, [ Validators.required, Validators.email ]],
      Senha: [null, [ Validators.required, Validators.minLength(8) ]]
    });
  }

  async login(event: Event) {
    event.preventDefault();

    this.carregando = true;
    
    if(this.formulario.valid){
      const response: LoginResponseModel = await this.servidor.chamarApi('api/acesso/login/', this.formulario.value);
      if (response.Sucesso) {
        localStorage.setItem("access_token",response.Token);
        this.router.navigate(['/Admin']);
        this.formulario.reset();
        this.carregando = false; 
      } else {
        alert("Erro!"+response.Mensagem);
        this.carregando = false;
        console.error("Erro ->", response.Mensagem);
      }
    }

  }

  cad(){
    this.router.navigate(['/Cadastro']);
  }



}
