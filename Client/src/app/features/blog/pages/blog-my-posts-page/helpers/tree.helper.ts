import {FlatTreeControl} from "@angular/cdk/tree";
import {MatTreeFlatDataSource, MatTreeFlattener} from "@angular/material/tree";

export interface FileObjectNode {
  id?: number,
  title?: string,
  isFolder?: boolean,
  parentId?: number,
  children?: FileObjectNode[]
}

export interface FlatFileObjectNode {
  id?: number;
  title: string;
  level: number;
  expandable: boolean;
  isFolder?: boolean;
  parentId: number;
}

const nodeMapDirect = new Map<FileObjectNode, FlatFileObjectNode>();
const nodeMapBack = new Map<FlatFileObjectNode, FileObjectNode>();

const transformer = (node: FileObjectNode, level: number): FlatFileObjectNode => {
  const flat: FlatFileObjectNode = {
    title: node.title ?? '',
    level: level,
    expandable: !!node.isFolder && !!node.title,
    isFolder: node.isFolder,
    id: node.id,
    parentId: node.parentId ?? 0
  };
  nodeMapDirect.set(node, flat);
  nodeMapBack.set(flat, node);
  return flat;
};

const treeFlattener = new MatTreeFlattener(
  transformer,
  (node: FlatFileObjectNode) => node.level,
  (node: FlatFileObjectNode) => node.expandable,
  (node: FileObjectNode) => node.children,
);

export const treeControl = new FlatTreeControl<FlatFileObjectNode>(
  (node: FlatFileObjectNode) => node.level,
  (node: FlatFileObjectNode) => node.expandable,
);

export const getFlatNode = (node: FileObjectNode) => nodeMapDirect.get(node);
export const getNode = (flat: FlatFileObjectNode) => nodeMapBack.get(flat);

export const dataSource = new MatTreeFlatDataSource(treeControl, treeFlattener);
export const hasChild = (_: number, node: FlatFileObjectNode) => node.expandable;
export const newNode = (_: number, node: FlatFileObjectNode) => !node.title;
