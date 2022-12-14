import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable, share} from "rxjs";
import {environment} from "../../../../environments/environment";
import {Post} from "../models/post";
import {PostRecent} from "../models/post-recent";

@Injectable()
export class PostApiService {

  constructor(private http: HttpClient) {
  }

  public SavePost(post: Post): Observable<any> {
    return this.http.post<any>(environment.serverUri + '/blog/post', post);
  }

  public GetPostsByProfileId(): Observable<Post[]> {
    return this.http.get<Post[]>(environment.serverUri + '/blog/all')
  }

  public GetPostById(id: number): Observable<Post> {
    return this.http.get<Post>(environment.serverUri + '/blog/' + id);
  }

  public CreateNewFolder(parentId: number, title: string): Observable<any> {
    return this.http.post(environment.serverUri + '/blog/folders/new?title=' + title +'&parentId=' + parentId, {});
  }

  public DeleteFile(fileId: number): Observable<any> {
    return this.http.delete(environment.serverUri + '/blog?fileId=' + fileId).pipe(share());
  }

  public FindPosts(searchString: string): Observable<any> {
    return this.http.get(environment.serverUri + '/blog/post/find?searchString=' + searchString).pipe(share());
  }

  public GetRecentPosts(): Observable<PostRecent[]> {
    return this.http.get<PostRecent[]>(environment.serverUri + '/blog/recent').pipe(share());
  }
}
