# ExtendedTextBox
Missing Text Boxes which are not available in microsoft's WPF.

## Installation
You can install it directly from nuget.org
* [Aptof.Controls](https://www.nuget.org/packages/Aptof.Controls/)

## Examples
Length property of NumberBox limit the length of number i.e. if Length is set to 5 it will not let user enter number beyond 5 digit
```xaml
        .................
        xmlns:aptof="http://www.aptof.com/"/>
        
    <Grid>
        
        <aptof:NumberBox Number="1001" Lenght="10" VerticalAlignment="Top" HorizontalAlignment="Left" Width="150" Margin="200,50,0,0" />
        
        <aptof:CurrencyBox Value="200" VerticalAlignment="Top" HorizontalAlignment="Left" Width="150" Margin="200,100,0,0" />
    </Grid>
    .........
```
## Important Information
Kindly note that Number of NumberBox is string and Value of CurrecyBox is Decimal

## Information
Works great with mahapps metro theme

## Release Note
Version 1.0.8: Bug Fixes: CurrencyBox Value property does not support binding.

Version 1.0.7: Bug Fixes: NumberBox does not support tab.

Version 1.0.6: null bug fixes

Version 1.0.5: Added Global Currency Support

## Issue
Mahapps TextBoxHelper not supported yet.
