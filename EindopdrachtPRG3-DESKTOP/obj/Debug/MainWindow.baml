   M S B A M L   `   `   `  ����L  HEindopdracht PRG3, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Eindopdracht_PRG3Eindopdracht_PRG3  5   	   "    Eindopdracht_PRG3.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:Eindopdracht_PRG3     5   	     � Title$  Partijen Overzicht��5   	   $	��700��6$   $
��1050��61   .��5	      � ��5
   
   �� 5       �� ImageSource$ Images/MainWindow.jpg��6   6?   .+�5   
   �� -   5         ��Name$ myTable��5      .+�5      �� -   5        $ title��5   2    �� Orientation$ 
Horizontal=�6   .+�5      �� 
�������
���   �$��20(�6A   $
��Bold"�6O   $	��100��6a    �� TextAlignment$ Center=�6m   
Partij nr.6�   �� 5      
�������
���   �$��20(�6G   $
��Bold"�6U   $	��200��6g   $ Center=�6s   Wit6�   �� 5      
���   �
�������$��20(�6B   $
��Bold"�6P   $	��200��6b   $ Center=�6n   Zwart6�   �� 5      
��� ���
���� ��$��20(�6D   $
��Bold"�6R   $	��180��6d   $ Center=�6p   Datum6�   �� 5      
���  ��
��� ���$��20(�6A   $
��Bold"�6O   $��80��6a   $ Center=�6l   Tijd6�   �� 5      
���**��
��� ���$��20(�6C   $
��Bold"�6Q   $	��150��6c   $ Center=�6o   Tijd per speler6�   �� 5      
����  �
����·�$��20(�6H   $
��Bold"�6V   $��90��6h   $ Center=�6s   	Uitslag6�   5      5      �� 5   
   -   5        $��Right=�5      $��Bottom=�6-   $	��150��6H   $��60��6T   $��
Create new��6`   $��20(�6u   $��Black"�6�   
���P��6�   �� 5   
   -   5        $��Right=�5      $��Bottom=�6-   $	��150��6H   $��60��6T   $��	0,0,200,0q�6`   $
��Zoek��6s   $��20(�6�   $��Black"�6�   
���   �
��� ���6�   �� 5   
   -   5         ��Name$ 
searchWord��5      $��Right=�6&   $��Bottom=�6B   $��
0,0,400,10q�6]    �� TextAlignment$ Center=�6q   $��20(�6�   $	��200��6�   $��40��6�   6�   �� 5   
   -   5         ��Name$ uitslagSelect��5      $��Right=�6*   $��Bottom=�6F   $��
0,0,400,10q�6a   $��20(�6u   $	��200��6�   $��40��6�   $y�Hidden=�6�   .@�5      �� .��6   	Winst wit6&   �� 5      .��6   Remise6#   �� 5      .��6   Winst zwart6(   5      �� 5    
   -   5        $��Right=�5       $��Bottom=�6-   $	��150��6H   $��60��6T   $��	0,0,820,0q�6`   $��Herstel��6s   $��20(�6�   $��Black"�6�   
����ح�
����  �6�   �� 5!   
   -   5        $ kleinergelijkgroter��5!      $y�Hidden=�60   $��Right=�6D   $��Bottom=�6`   $��
0,0,400,70q�6{   $��16(�6�   $	��350��6�   $��40��6�   .@�5"      �� .��6   -+Kleiner dan/Minder dan/Voor deze datum/tijd6H   �� 5#      .��6   " Is gelijk aan/Op deze datum/tijd6=   �� 5$      .��6   (&Groter dan/Meer dan/Na deze datum/tijd6C   5%      �� 5&   
   -	   5         ��Name$ beschrijving��5&      $y�Hidden=�6*   $��Right=�6>   $��Bottom=�6Z   $��
0,0,400,70q�6u   $��16(�6�   $	��200��6�   $��20��6�   Typ spelersnaam:6�   �� 5'   
   -
   5        $ 
zoekdingen��5'      $��Right=�6'   $��Bottom=�6C   $��
0,0,610,10q�6^   $��21(�6r   $	��200��6�   $��40��6�   .@�5(      �� .��6   Naam6!   �� 5)      .��6   Partijnummer6)   �� 5*      .��6   Datum6"   �� 5+      .��6   Tijd6!   �� 5,      .��6   Spelerstijd6(   �� 5-      .��6   	Uitslag6$   5.      5/      50      