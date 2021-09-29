import { Pipe, PipeTransform } from '@angular/core';
import { ZanrDetail} from '../app/shared/zanr-detail.model';

@Pipe({
  name: 'searchfilter'
})
export class SearchfilterPipe implements PipeTransform {

  transform(Zanr: ZanrDetail[],searchValue:string): any {
    if(!Zanr||!searchValue)
    {
      return Zanr;
    }
    return Zanr.filter(zanr=>
      zanr.naziv.toLocaleLowerCase().includes(searchValue.toLocaleLowerCase()));
      
  }
}
