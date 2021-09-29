import { Component, OnInit } from '@angular/core';
import { ZanrDetailService } from '../shared/zanr-detail.service';
import { ZanrDetail } from '../shared/zanr-detail.model';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-zanr-details',
  templateUrl: './zanr-details.component.html',
  styles: [
  ]
})
export class ZanrDetailsComponent implements OnInit {

  searchValue: string='';
  

  constructor(public service: ZanrDetailService,
    private toastr: ToastrService) { }

  ngOnInit(): void {
    this.service.refreshList();
    
  }

  populateForm(selectedRecord: ZanrDetail) {
    this.service.formData = Object.assign({}, selectedRecord);
  }

  onDelete(id: number) {
    if (confirm('Da li ste sigurni da zelite obrisati zanr?')) {
      this.service.deleteZanrDetail(id)
        .subscribe(
          res => {
            this.service.refreshList();
            this.toastr.error("Uspjesno obrisano!", 'Obavjestenje');
          },
          err => { console.log(err) }
        )
    }
  }
  

}
