import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {environment} from "../../../../environments/environment";
import {Post} from "../models/post";

@Injectable()
export class PostApiService {

  constructor(private http: HttpClient) {
  }

  public GetPostsByProfileId(): Observable<Post[]> {
    return this.http.get<Post[]>(environment.serverUri + '/blog/all')
  }
}
