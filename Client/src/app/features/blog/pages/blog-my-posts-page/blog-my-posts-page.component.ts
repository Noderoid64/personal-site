import {Component} from '@angular/core';
import {
  treeControl,
  dataSource,
  hasChild,
  newNode,
  getNode,
  getFlatNode,
  FlatFileObjectNode
} from './helpers/tree.helper';
import {PostApiService} from "../../services/post-api.service";

@Component({
  selector: 'app-blog-my-posts-page',
  templateUrl: './blog-my-posts-page.component.html',
  styleUrls: ['./blog-my-posts-page.component.scss']
})
export class BlogMyPostsPageComponent {

  public content: string | null = null;
  public selectedId: number | undefined;
  public dataSource = dataSource;
  public treeControl = treeControl;
  public hasChild = hasChild;
  public newNode = newNode;
  private rawData: any;

  constructor(private postApi: PostApiService) {
    postApi.GetPostsByProfileId().subscribe(x => {
      this.rawData = x;
      this.dataSource.data = [this.rawData];
    })
  }

  public onNewFolder(node: FlatFileObjectNode): void {
    const rawNode = getNode(node)!;
    const newFolder = {
      isFolder: true,
      parentId: rawNode.id
    };
    rawNode.children?.push(newFolder);
    this.dataSource.data = [this.rawData];
    const toExpand = this.treeControl.dataNodes.find(x => x.id == node.id);
    this.treeControl.expand(toExpand!);
  }

  public onNewFolderSave($event: any, flatNode: FlatFileObjectNode): void {
    if ($event.target.value && $event.target.value != '') {
      const node = getNode(flatNode)!;
      this.postApi.CreateNewFolder(node.parentId!, $event.target.value).subscribe(x => {
        flatNode.isFolder = true;
        flatNode.title = $event.target.value;
        this.dataSource.data = [this.rawData];
        const toExpand = this.treeControl.dataNodes.find(x => x.id == flatNode.id);
        this.treeControl.expand(toExpand!);
      });
    }
  }

  public onFolderDelete(flatNode: FlatFileObjectNode): void {
    this.postApi.DeleteFile(flatNode.id!).subscribe(x => {
      const node = getNode(flatNode)!;
      this.dataSource.data = [this.rawData];
      const toExpand = this.treeControl.dataNodes.find(x => x.id == node!.parentId);
      this.treeControl.expand(toExpand!);
    });
  }

  public onPostClick(nodeId: number): void {
    this.postApi.GetPostById(nodeId).subscribe(x => {
      this.content = x.content;
      this.selectedId = x.id;
    })
  }
}
