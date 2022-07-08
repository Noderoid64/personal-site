import {AccessType} from "./access-type.enum";

export interface Post {
  id?: number,
  content: string,
  title?: string,
  accessType?: AccessType
}
