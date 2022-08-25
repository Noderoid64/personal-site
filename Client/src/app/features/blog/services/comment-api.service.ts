import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable, share} from "rxjs";
import {environment} from "../../../../environments/environment";
import {Comment} from "../models/comment";

@Injectable({providedIn: 'root'})
export class CommentApiService {

  constructor(private http: HttpClient) {
  }

  public GetComments(postId: number): Observable<Comment[]> {
    return this.http.get<Comment[]>(environment.serverUri + '/comments/posts/' + postId).pipe(share());
  }

  public PostComment(postId: number, content: string): Observable<any> {
    return this.http.put(environment.serverUri + '/comments/posts/' + postId, {content}).pipe(share());
  }

  public DeleteComment(commentId: number): Observable<any> {
    return this.http.delete(environment.serverUri + '/comments/' + commentId).pipe(share());
  }
}
