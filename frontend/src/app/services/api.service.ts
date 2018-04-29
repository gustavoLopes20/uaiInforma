import { Injectable } from '@angular/core';
import { Http, Request, Response, RequestOptionsArgs, Headers, BaseRequestOptions, RequestOptions } from '@angular/http'

import { environment } from '../../environments/environment';

import 'rxjs/Rx';

@Injectable()
export class ApiService {

  constructor(private http: Http) {

  }

  private mapUrl(url: string): string {

    if (!url.startsWith('/')) {
      url = '/' + url;
    }

    let prodUrl = window.location.protocol + '//' + window.location.host;
    let devUrl = 'http://192.168.0.103:54142';
    let apiUrl = '';

    if (environment.production) {
      apiUrl = prodUrl;
    } else {
      apiUrl = devUrl;
    }

    url = apiUrl + url;
    return url;
  }


  async chamarApi(api: string, postData?: Object) : Promise<any> {

    let access_token:string = localStorage.getItem('access_token');

    let options: RequestOptionsArgs = {
      url: this.mapUrl(api),
      headers: new Headers({
        'access_token': access_token
      })
    };

    if (postData) {
      options.method = 'POST';
      options.body = postData;

      try {
        const response:any = await this.http.post(this.mapUrl(api), postData, options).toPromise();
        return JSON.parse(response._body);
      } catch (e) {
        return { sucesso:false , mensagem: 'Erro no servidor.'};
      }

    } else {
      options.method = 'GET';

      try {
        const response:any = await this.http.get(this.mapUrl(api), options).toPromise();
        return JSON.parse(response._body);
      } catch (e) {
        return { sucesso: false , mensagem: 'Erro no servidor.'};
      }
    }
  }

  async getUri(uri: string) : Promise<any>{
    try{
      const response: any = await this.http.get(uri).toPromise();
      return JSON.parse(response._body);
    }catch(err){
      return { Sucesso: false, Mensagem: err};
    } 
  }

}
