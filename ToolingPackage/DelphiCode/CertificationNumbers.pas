unit CertificationNumbers;

interface

type
  TCertificationYear = (cy2016, cy2017, cy2018);

const
  RL1CertificationNumber: array[TCertificationYear] of string =
    ('RQ-16-01-072',
     'RQ-17-01-032',
     'RQ-18-01-209');

  RL2CertificationNumber: array[TCertificationYear] of string =
    ('RQ-16-02-032',
     'RQ-17-02-014',
     'RQ-18-02-098');

function YearToCertificationYear(Year: Integer): TCertificationYear;

implementation

function YearToCertificationYear(Year: Integer): TCertificationYear;
begin
  case Year of
    2016: Result := cy2016;
    2017: Result := cy2017;
    2018: Result := cy2018;
  else
    Result := High(TCertificationYear);
  end;
end;

end.