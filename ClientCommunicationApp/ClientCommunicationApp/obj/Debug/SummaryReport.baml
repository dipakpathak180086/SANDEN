   M S B A M L   `   `   `  ����E  AInvoiceApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:InvoiceApp
InvoiceApp  5   	       InvoiceApp.SummaryReport   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:InvoiceApp     5   	   -   5          � Title$  Summary Report��5   	   $	��488��6    $	��783��6-    	  MyWin#���   69    � WindowStartupLocation$ CenterScreen=�6Z   ���	#FF007ACC$��1q�6�    � Icon$ Images/1.png��6�   .��5	      � ���	#FF313131.+�5
   
   �� -   5        $��View Summary Report��5
      $	��Top=�6.   $��Stretch=�6F   $
��Auto��6e   ���	#FFEFF3F7$��14(�6�   ���	#FF0479DA$��0,0,0,1q�6�   $��Center=�6�   ���	#FF0348786
  �� 5   
   -   5         ��Name$ btnClose��5      $��X��6#   $	��Top=�6/   $��	743,3,3,0q�6G   $��20��6Z     btnPlain#��[ 6f   $��0q�6�   6�   �� 5   
   -   5         ��Name$ label��5      $��Filter��6   $
��Left=�60   $��	19,34,0,0q�6K   $	��Top=�6^   $��26��6v   �� 5   
   -   5        $ label_Copy5��5      $��Filter Value��6%   $
��Left=�6<   $��
241,38,0,0q�6W   $	��Top=�6k   $��26��6�   �� 5   
   -   5         ��Name$ txtFilterValue��5      $
��Left=�6*   $��23��6E   $��
353,39,0,0q�6Q    �� TextWrapping$
 Wrap=�6e   $	��Top=�6y   $	��155��6�    �� CharacterCasing$ Upper=�6�   $��1��6�   �� 5   
   -   5         ��Name$ 	cmbFilter��5      $
��Left=�6&   $��	74,39,0,0q�6A   $	��Top=�6T   $	��155��6l   $��4��6x   $��23��6�   �� 5   
   -   5        $ btnGet��5      $	��Get��6!   $
��Left=�6/   $��
538,40,0,0q�6J   $	��Top=�6^   $��70��6v   	 � ToolTip$	 Add Item To List��6�   $��20��6�   $��7��6�   6�   �� 5   
   -	   5        $ btnReset��5      $��Reset��6#   $
��Left=�63   $��
538,83,0,0q�6N   $	��Top=�6b   $��70��6z   $	 Reset��6�   
 J� RenderTransformOrigin$
 -11.545,7.65&�6�   $��20��6�   6�   &   System.Windows.Controls.DataGrid  5   
   -
   5          Name$ dg��5      $��Stretch=�6   $��10,114,10,10q�6=      CanUserResizeRows .     CanUserAddRows .     ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 6�      AutoGenerateColumns .     
IsReadOnly .    CanUserResizeColumns .  $��Stretch=�6�   ��6�   Z�    Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ TinNo��6?   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6%   �� *TinNo+6?     5      $ PanNo��6?    6%   �� *PanNo+6?     5      $ BillNo��6@    6%   �� *BillNo+6@     5      $ InvoiceDate��6E    6%   �� *InvoiceDate+6E     5      $ Address��6A    6%   �� *	Address+6A     5      $
 PoNo��6>    6%   �� *PoNo+6>     5      $ ItemDesc��6B    6%   �� *
ItemDesc+6B     5      $	 Qty��6=    6%   �� *Qty+6=     5      $
 Rate��6>    6%   �� *Rate+6>     5      $ Amount��6@    6%   �� *Amount+6@   5      (  "System.Windows.Controls.DatePicker  5    
   -   5          Name$ FromDate��5       $
��Left=�6'   $��	74,78,0,0q�6B   $	��Top=�6U   $	��155��6m   �� 5!   
   -   5        $ 
label_Copy��5!      $
��From��6$   $
��Left=�63   $��	19,76,0,0q�6N   $	��Top=�6a   $��26��6y     5"   
   -   5        $ ToDate��5"      $
��Left=�6%   $��
356,79,0,0q�6@   $	��Top=�6T   $	��155��6l   �� 5#   
   -   5        $ label_Copy1��5#      $��To��6%   $
��Left=�62   $��
307,77,0,0q�6M   $	��Top=�6a   $��26��6y   �� 5$   
   -   5        $ btnItemCount��5$      $��0��6'   $��Right=�63   $��	0,62,10,0q�6O   $	��Top=�6b   $��57��6z   $	 Selected Item Count��6�   $
 -11.545,7.65&�6�   $��48��6�   $��45454��6�   $
��Bold"�6�   $��20(�6�   �� 5%   
   -   5        $ label_Copy2��5%      $��*��6%   $
��Left=�61   $��	53,68,0,0q�6L   $	��Top=�6_   $��23��6w   $��21��6�   $
��Bold"�6�   $��16(�6�   ���	#FFD430306�   �� 5&   
   -   5        $ label_Copy3��5&      $��*��6%   $
��Left=�61   $��
330,68,0,0q�6L   $	��Top=�6`   $��23��6x   $��21��6�   $
��Bold"�6�   $��16(�6�   ���	#FFD430306�   5'      5(      