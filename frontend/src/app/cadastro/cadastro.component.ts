import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DefaultResponseModel } from '../data/dataModels';
import { ApiService } from '../services/api.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-cadastro',
  templateUrl: './cadastro.component.html',
  styleUrls: ['./cadastro.component.scss']
})
export class CadastroComponent implements OnInit {

  public formulario:FormGroup;
  public carregando: boolean = false;
  public unidades:Array<any> = [];

  constructor(
    private servidor:ApiService,
    private router:Router,
    private formBuilder:FormBuilder,
  ) { }

  ngOnInit() {
    this.formulario = this.formBuilder.group({ 
      Email: [null, [ Validators.required, Validators.email ]],
      NomeUsuario: [null, [ Validators.required, Validators.minLength(3) ]],
      Senha: [null, [ Validators.required, Validators.minLength(8) ]],  
      Csenha: [null, [ Validators.required, Validators.minLength(8) ]],
    });
  }


  async cadastro(event: Event) {
    event.preventDefault();
    this.carregando = true;

    if(this.formulario.valid){
      const response:DefaultResponseModel = await this.servidor.chamarApi('api/acesso/cadastro', this.formulario.value);

      if (response.Sucesso) {
        alert(response.Mensagem);
        this.router.navigate(['/Login']);
        this.formulario.reset();
      } else{
        console.error("Erro:",response.Mensagem);
        alert(response.Mensagem);
      }    
      this.carregando = false; 
    }
  }
  
}
