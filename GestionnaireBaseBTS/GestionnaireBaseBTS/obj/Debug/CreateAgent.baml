   M S B A M L   `   `   `  ����N  JGestionnaireBaseBTS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:GestionnaireBaseBTSGestionnaireBaseBTS  5   	   %    GestionnaireBaseBTS.CreateAgent   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:GestionnaireBaseBTS     5   	   ���#BEBEBE  � Title$  Création agent��5   	   $	��300��6    $	��500��6-    � Icon$ Images/User-Profile.png��69    � 
ResizeMode$ NoResize=�6X   .��5
      � ���#BEBEBE$��10q�6     � RowDefinitions 5   
   �� 5      $��30 �6   �� 5      $��30 �6   �� 5      $��30 �6   �� 5      $��30 �6   �� 5      $��30 �6   �� 5      $��30 �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $	��185 �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $��0��6$   $��Nom :��61   ���	#FF0F6A99$��5q�6U   �� 5   
   $��0��6   $��1��6$   $��Prenom :��61   ���	#FF0F6A99$��5q�6X   �� 5   
   $��0��6   $��2��6$   $��Login :��61   ���	#FF0F6A99$��5q�6W   �� 5   
   $��0��6   $��3��6$   $��
Password :��61   ���	#FF0F6A99$��5q�6Z   �� 5   
   $��0��6   $��4��6$   $��
Civilite :��61   ���	#FF0F6A99$��5q�6Z   �� 5   
   $��0��6   $��5��6$   $��Email :��61   ���	#FF0F6A99$��5q�6W   �� 5!   
   -   5         ��Name$ 
tbNomAgent��5!   y   $��1��6   $��0��6"   $��5q�6n   ��6/   �� *
NomAgent+ �� UpdateSourceTrigger$ PropertyChanged=�6y   �� 5"   
   -   5        $ tbPrenomAgent��5"   |   $��1��6   $��1��6"   $��5q�6q   ��6/   �� *PrenomAgent+$ PropertyChanged=�6|   �� 5#   
   -   5        $ tbPseudoAgent��5#   |   $��1��6   $��2��6"   $��5q�6q   ��6/   �� *PseudoAgent+$ PropertyChanged=�6|   �� 5$   
   -   5        $ tbPasswordAgent��5$   ~   $��1��6   $��3��6"   $��5q�6s   ��6/   �� *PasswordAgent+$ PropertyChanged=�6~   �� 5%   
   -   5        $ tbCiviliteAgent��5%   ~   $��1��6   $��4��6"   $��5q�6s   ��6/   �� *CiviliteAgent+$ PropertyChanged=�6~   �� 5&   
   -   5        $ tbEmailAgent��5&   {   $��1��6   $��5��6"   $��5q�6p   ��6/   �� *
EmailAgent+$ PropertyChanged=�6{   �� 5(   
   -   5        $��6��5(      $��3��6   $	��200��62   $��0,20,0,0q�6>   $��Center=�6P   .��5)      ��  �� Orientation$ 
Horizontal=�6   .+�5*      �� $��Creer��6   ���	#FF0F6A99$
��Bold"�6@   �� 5+      $#��Images/Arrowhead-Right-01.png��6   $��10��6?   $��10,0,0,0q�6J   5,      5-      �� 5/   
   $��Listes des agents :��6   $��2��6/   $��5,0,0,0q�6?   ���	#FF0F6A996P   �� 50   
   -   5         ��Name$ 	lstAgents��50      $��2��6&   $��6��66   $��5,20,5,5q�6G   ��.  $��Visible=�6   $��Visible=�6�   ��51      �� 52      .R�53      ]� $��10 5 0 5q�6    .+�54      ]� $	��Top=�6$   .+�55      �� $
��Left=�6(   $��14(�6]   $��CharacterEllipsis=�6k   $��DemiBold"�6�   ��6>   �� *CiviliteAgent+6�   �� 56      $�� ��6(   �� 57      $
��Left=�6(   $��14(�6[   $��CharacterEllipsis=�6i   $��DemiBold"�6�   ��6>   �� *PrenomAgent+6�   �� 58      $�� ��6(   �� 59      $
��Left=�6(   $��14(�6X   $��CharacterEllipsis=�6f   $��DemiBold"�6�   ��6>   �� *
NomAgent+6�   5:      �� 5;      $ 
Horizontal=�6%   .+�5<      �� ���	#FF777777��6(   �� *PseudoAgent+6E   �� 5=      $	�� - ��6(   ���	#FF77777763   �� 5>      ���	#FF777777��6(   �� *
EmailAgent+6D   5?      5@      5A      5C      5D      5E      