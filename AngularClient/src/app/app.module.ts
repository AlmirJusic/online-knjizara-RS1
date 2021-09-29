import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from "@angular/forms";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';

import { AppComponent } from './app.component';
import { ZanrDetailsComponent } from './zanr-details/zanr-details.component';
import { ZanrDetailFormComponent } from './zanr-details/zanr-detail-form/zanr-detail-form.component';
import { HttpClientModule } from '@angular/common/http';
import { SearchfilterPipe } from '../app/searchfilter.pipe';

@NgModule({
  declarations: [
    AppComponent,
    ZanrDetailsComponent,
    ZanrDetailFormComponent,
    SearchfilterPipe
   
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot()
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
