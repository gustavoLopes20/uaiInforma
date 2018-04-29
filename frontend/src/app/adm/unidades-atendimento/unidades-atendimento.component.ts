import { Component, OnInit } from '@angular/core';
import { AdmComponent } from '../adm.component';
import { ApiService } from '../../services/api.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-unidades-atendimento',
  templateUrl: './unidades-atendimento.component.html',
  styleUrls: ['./unidades-atendimento.component.scss']
})
export class UnidadesAtendimentoComponent implements OnInit {

  public formulario: FormGroup;
  public lista: Array<any> = [];
  public salvando: boolean = false;
  public model:any = {};
  public editar:boolean = false;

  constructor(
    private admComponent: AdmComponent,
    private servidor: ApiService,
    private formBuilder: FormBuilder,
  ) { }


  async ngOnInit() {
    this.formulario = this.formBuilder.group({
      Descricao : [null, Validators.required],
      Endereco: [null, Validators.required],
      Bairro: [null, Validators.required],
      Telefone: [null, Validators.required],
    });
    this.lista = await this.servidor.chamarApi("api/Unidades", null);
  }

  //novo item
  novoItem() {
    this.formulario.reset();
  }

  //salvando item
  async salvarItem() {

    if (this.formulario.valid) {
      //enviando os dados 
      const response: any = await this.servidor.chamarApi('api/Unidades', this.formulario.value);

      if (response.Sucesso) {
        this.salvando = false;
        this.formulario.reset();
        this.lista = await this.servidor.chamarApi("api/Unidades", null);
      }else{
        this.salvando = false;
        alert(response.Mensagem);
      }

    }

  }

  //excluindo item
  async excluirItem() {

    if (this.editar) {

      if (confirm("Deseja Realmente Excluir?")) {
        this.editar = false; 
          let post: any = await this.servidor.chamarApi('api/Unidades/Delete', this.model);
          let response: any = JSON.parse(post._body);

          if (response.sucesso) {
            this.salvando = false;
            this.formulario.reset();
            this.lista = await this.servidor.chamarApi("api/Unidades", null);
          }else{
            this.salvando = false;
            alert(response.Mensagem);
          }
      }

    }
  }

  //editando item
  editarItem(item: any) {
    this.editar = true;
    this.model = item;
    this.formulario.setValue({
      Descricao : item.Descricao,
      Endereco: item.Endereco,
      Bairro: item.Bairro,
      Telefone: item.Telefone,
    });
  }

}
