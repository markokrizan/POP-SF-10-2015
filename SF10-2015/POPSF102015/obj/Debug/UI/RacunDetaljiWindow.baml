   M S B A M L   `   `   `  ����B  >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:WpfApp1.UI
WpfApp1.UI  5   	   #    WpfApp1.UI.RacunDetaljiWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:WpfApp1.UI     5   	     � Icon$  ../Images/Racun.png��5   	   
���   � � WindowStartupLocation$ CenterOwner=�5	   	    � Title$ Stavke Racuna��5
   	   $	��350��6   $��331.262��6,    � 
ResizeMode$ NoResize=�5   	    � WindowState$ Normal=�5   	   .��5      � $��0,0,-4.6,0.8q�6    � RowDefinitions 5   
   �� 5      $	��10* �6   �� 5      $	��13* �6   .+�5   
   �� $��	Namestaj:��6   $
��Left=�6$   $��	10,17,0,0q�6?   $	��Top=�6R   �� 5   
   $��Dodatne usluge:��6   $
��Left=�6*   $��	10,10,0,0q�6E   $	��Top=�6X   $��1��6p   �� 5   
   -   5        $��lbUsluge��5      $
��Left=�6"   $��87��6=   $��	21,41,0,0q�6I   $	��Top=�6\   $	��286��6t   $��1��6�   �� 5   
   -   5        $��Izlaz��5      $
��Left=�6!   $��213,141,0,0q�6<   $	��Top=�6Q   $��94��6i   $��31��6t   $��1��6�   &   System.Windows.Controls.DataGrid  5   
   -   5        $��dgStavkeNamestaja��5         CanUserAddRows .     
IsReadOnly .    AutoGenerateColumns . $
��Left=�6�   $	��100��6�   $��	21,48,0,0q�6�   $	��Top=�6  $	��286��6  $��2��6%  5      5      