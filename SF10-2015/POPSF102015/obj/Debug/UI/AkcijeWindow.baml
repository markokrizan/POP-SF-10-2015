   M S B A M L   `   `   `  ����B  >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:WpfApp1.UI
WpfApp1.UI  5   	       WpfApp1.UI.AkcijeWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:WpfApp1.UI     5   	     � Title$  Izmeni/Dodaj��5   	   $��336.2��6   $	��270��6-    � 
ResizeMode$ NoResize=�5	   	    � WindowState$ Normal=�5
   	    � WindowStartupLocation$ CenterOwner=�5   	    � Icon$ ../Images/Edit.png��5   	   
���   �.��5      � $��0,0,3.6,0.2q�6   .+�5   
   �� $��0��6   $��0��6   $��Naziv: ��6-   $��15(�6?   $
��Left=�6M   $��62��6h    J� RenderTransformOrigin$ 0.786,0.671&�6s   $��20,25,0,209.8q�6�   �� 5   
   -   5        $��tbNaziv��5   e   $��0��6/   $��15(�6<   $
��Left=�6J   $	��124��6t   $��82,25,0,249q�6�   ��6   ��  �� Path$ Naziv�6�   �� 5   
   -   5        $��Sacuvaj��5      $��Right=�6#   $��0,253,135,22q�6U   $��90��6k   �� 5   
   -   5        $��Izlaz��5      $��Right=�6!   $��0,253,32,22q�6=   $��90��6R   $ 0.184,0.766&�6]   6�   �� 5   
   $��Popust: ��6   $��15(�6#   $
��Left=�61   $	��Top=�6L   $��41��6d   $��67��6p   $��	15,76,0,0q�6{   $ 2.081,1.156&�6�   �� 5   
   $��	Pocetak: ��6   $��15(�6$   $
��Left=�62   $	��Top=�6M   $��41��6e   $��67��6q   $��
10,126,0,0q�6|   $ 2.081,1.156&�6�   �� 5   
   $��Kraj: ��6   $��15(�6!   $
��Left=�6/   $	��Top=�6J   $��41��6b   $��67��6n   $��
35,171,0,0q�6y   $ 2.081,1.156&�6�   �� 5   
   -   5        $��tbPopust��5   �   $��15(�6L   $
��Left=�6Z   $��32��6u   $��	82,76,0,0q�6�    �� TextWrapping$
 Wrap=�6�   $	��Top=�6�   $	��124��6�   ��6   �� $ Popust� �� ValidatesOnExceptions . 6�   (  "System.Windows.Controls.DatePicker  5   
   -   5        $��	dpPocetak��5      $
��Left=�6R   $��
82,126,0,0q�6m   $	��Top=�6�   $	��124��6�   $��32��6�   	   SelectedDate	 6'   �� $ DatumPocetka�6�     5   
   -   5        $��dpKraj��5      $
��Left=�6R   $��
82,171,0,0q�6m   $	��Top=�6�   $	��124��6�   $��32��6�   	 6$   �� $ DatumZavrsetka�6�   5      5      