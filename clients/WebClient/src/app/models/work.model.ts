export interface IWork {
     Id: string;
     Title: string;
     Type: string;
     Language: string;
     Aspects: Array<IAspect>;
     SectionLevels: Array<ISectionLevel>;
}

export interface IAspect {
     Id: string;
     Title: string;
     Description: string,
     Value: string,
     Color: string,
     Symbol: string,
     IsEnabled: string,
     IsSearchable: boolean,
     Searchstring: string,
     IsGroup: boolean,
     IsChildrenOrdered: boolean,
     OrderNo: number,
     Aspects: Array<IAspect>;
     Relations: Array<IRelation>;
}

export interface IRelation {
     FromId: string,
     ToId: string,
     Title: string,
     Description: string,
}

export interface ISectionLevel {
     Id: string;
     Title: string;
     Description: string,
     Color: string,
     Level: number
}

export interface IWorksColl {
     Works: Array<IWork>;

}
