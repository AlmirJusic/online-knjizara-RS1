import { Injectable } from '@angular/core';
import { ZanrDetail } from './zanr-detail.model';
import { HttpClient } from "@angular/common/http";
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ZanrDetailService {

  constructor(private http: HttpClient) { }

  readonly baseURL = 'https://localhost:44356/api/zanrangular'
  formData: ZanrDetail = new ZanrDetail();
  list!: ZanrDetail[];

  postZanrDetail() {
    return this.http.post(this.baseURL, this.formData);
  }

  putZanrDetail() {
    return this.http.put(`${this.baseURL}/${this.formData.id}`, this.formData);
  }

  deleteZanrDetail(id: number) {
    return this.http.delete(`${this.baseURL}/${id}`);
  }

  refreshList() {
    this.http.get(this.baseURL)
      .toPromise()
      .then(res =>this.list = res as ZanrDetail[]);
  }
  getAllZanr():Observable<ZanrDetail[]>{
    return this.http.get<ZanrDetail[]>(this.baseURL);
  }


}
