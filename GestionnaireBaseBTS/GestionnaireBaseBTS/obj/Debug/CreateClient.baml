   M S B A M L   `   `   `  ����N  JGestionnaireBaseBTS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:GestionnaireBaseBTSGestionnaireBaseBTS  5   	   &     GestionnaireBaseBTS.CreateClient   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:GestionnaireBaseBTS     5   	   ���#BEBEBE  � Title$  Création client��5   	   $	��300��6!   $	��300��6.   .��5	      � ���#BEBEBE$��10q�6     � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $	��200 �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $��0��6$   $��Nom :��61   ���	#FF0F6A99$��5q�6U   �� 5   
   $��0��6   $��1��6$   $��	Adresse :��61   ���	#FF0F6A99$��5q�6Y   �� 5   
   $��0��6   $��2��6$   $��Ville :��61   ���	#FF0F6A99$��5q�6W   �� 5   
   $��0��6   $��3��6$   $��Code postal :��61   ���	#FF0F6A99$��5q�6]   �� 5   
   $��0��6   $��4��6$   $��Email :��61   ���	#FF0F6A99$��5q�6W   �� 5   
   $��0��6   $��5��6$   $��Telephone :��61   ���	#FF0F6A99$��5q�6[   �� 5    
   $��0��6   $��6��6$   $��SQL :��61   ���	#FF0F6A99$��5q�6U   �� 5"   
   $��1��6   $��0��6"   $��5q�6i   ��6/   �� *Nom+ �� UpdateSourceTrigger$ PropertyChanged=�6i   �� 5#   
   $��1��6   $��1��6"   $��5q�6m   ��6/   �� *	Adresse+$ PropertyChanged=�6m   �� 5$   
   $��1��6   $��2��6"   $��5q�6k   ��6/   �� *Ville+$ PropertyChanged=�6k   �� 5%   
   $��1��6   $��3��6"   $��5q�6p   ��6/   �� *
CodePostal+$ PropertyChanged=�6p   �� 5&   
   $��1��6   $��4��6"   $��5q�6k   ��6/   �� *Email+$ PropertyChanged=�6k   �� 5'   
   $��1��6   $��5��6"   $��5q�6o   ��6/   �� *	Telephone+$ PropertyChanged=�6o   �� 5(   
   $��1��6   $��6��6"   $��5q�6i   ��6/   �� *SQL+$ PropertyChanged=�6i   �� 5*   
   -   5        $��7��5*      $��2��6   $	��200��62   $��0,10,0,0q�6>   .��5+      ��  �� Orientation$ 
Horizontal=�6   .+�5,      �� $��Valider��6   ���	#FF0F6A99$
��Bold"�6B   �� 5-      $#��Images/Arrowhead-Right-01.png��6   $��10��6?   $��10,0,0,0q�6J   5.      5/      50      51      