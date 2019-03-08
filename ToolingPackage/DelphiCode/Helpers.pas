unit Helpers;

interface

uses Classes;

type
  TXmlBoxNode = class(TCollectionItem)
  public
    Box: string;
    NodeName: string;
    Amount: Currency;
    Code: string;
    CodeNodeName: string;
    ParentNode: string;
  end;

  TXmlBoxNodes = class(TCollection)
  protected
    function GetItem(Index: Integer): TXmlBoxNode;
    procedure SetItem(Index: Integer; Value: TXmlBoxNode);
  public
    constructor Create;
    function Add: TXmlBoxNode;
    property Items[Index: Integer]: TXmlBoxNode read GetItem write SetItem; default;
  end;

implementation

{ TXmlBoxNodes }

function TXmlBoxNodes.Add: TXmlBoxNode;
begin
  Result := TXmlBoxNode(inherited Add);
end;

constructor TXmlBoxNodes.Create;
begin
  inherited Create(TXmlBoxNode);
end;

function TXmlBoxNodes.GetItem(Index: Integer): TXmlBoxNode;
begin
  Result := TXmlBoxNode(inherited GetItem(Index));
end;

procedure TXmlBoxNodes.SetItem(Index: Integer; Value: TXmlBoxNode);
begin
  inherited SetItem(Index, Value);
end;

end.

