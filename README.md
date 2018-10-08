# ExtendedTextBox
Missing Text Boxes which are not available in microsoft's WPF.

## Installation
You can install it directly from nuget.org
* [Aptof.Controls](https://www.nuget.org/packages/Aptof.Controls/)

## Examples
```
        .................
        xmlns:aptof="http://www.aptof.com/"/>
        
    <Grid>
        
        <aptof:NumberBox Number="1001" VerticalAlignment="Top" HorizontalAlignment="Left" Width="150" Margin="200,50,0,0" />
        
        <aptof:CurrencyBox Value="200" VerticalAlignment="Top" HorizontalAlignment="Left" Width="150" Margin="200,100,0,0" />
    </Grid>
    .........
```
## Important Information
Kindly note that Number of NumberBox is string and Value of CurrecyBox is Decimal

## Information
Works great with mahapps metro
