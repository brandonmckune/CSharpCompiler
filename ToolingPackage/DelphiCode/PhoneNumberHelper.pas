unit PhoneNumberHelpers;

interface

type
  TPhoneNumberHelpers = Class

  public
    class function AdjustCANPhoneNumber(phoneNumber : string) : string;

End;
implementation
  uses
    StrUtils;

  class function TPhoneNumberHelpers.AdjustCANPhoneNumber(phoneNumber : string) : string;
  begin
    if not ContainsText(phoneNumber, '-') then
      begin
         Result := copy(phoneNumber, 1, 3) + '-' + copy(phoneNumber, 4, 4);
      end
      else
      begin
         Result := phoneNumber;
      end;
  end;

end.