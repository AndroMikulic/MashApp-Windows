   M S B A M L   `   `   `  ���� B  >MashApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:MashAppMashApp      MashApp.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     'localclr-namespace:MashApp     Z VFontAwesome.WPF, Version=4.7.0.37774, Culture=neutral, PublicKeyToken=0758b07a11a4f466-fa$http://schemas.fontawesome.io/icons/    � Title$  MashApp Client��$��450.4��$	��800�����	#FF202020$	��800��$	��450�� � Icon$ MashAppIcon.ico�� � WindowStartupLocation$ CenterScreen=�.��� -    �Name$ mainGrid��$��0q����#202020.+�� -   $ firstTimeGrid��$y�	Collapsed=�.+��� -    ��Name$ 	introText��$��Hello!��$��0,184,0,180q� J� RenderTransformOrigin$ 0.5,0.5&�$��Dosis)�$��22(�$��Center=�$��Center=�$��0q� � UseLayoutRounding .  ���	#FFE6E6E6 J� Opacity$ 0Q��� -    ��Name$ 	placeName��$��Center=�$��Center=�$��Center=�$��40��$��0,243,0,137.8q�	 �� TextWrapping$
	 Wrap=�$��My Place��$��Center=�$	��256��$��Dosis)�$y�	Collapsed=�
���   ����	#FF202020���	#FFFBC02D$��18(�$ 0Q��� -   
 ��Name$
 nameEntered��$
��Done��$��Center=�$��	0,200,0,0q�$��Center=�$��75��$��Dosis)�$��26��
���   ����	#FF202020���	#FFFBC02D$��18(�$ 0Q�$y�	Collapsed=��� -    ��Name$ selectYoutube�� �� BitmapScalingMode$ HighQuality=�$��64��$��0,150,104,0q�$��Center=�$��Center=�$��64��$��youtube.png��$ 0.5,0.5&�$y�	Collapsed=�$ 0Q��� -   $ selectFolder��$ HighQuality=�$��64��$��0,150,-100,0q�$��Center=�$��Center=�$��64��$��
folder.png��$ 0.5,0.5&�$y�	Collapsed=�$ 0Q�� -   $ startupGrid��$y�	Collapsed=�.+��� -	   $ connectionLabel��$��
Connecting��$��
0,-122,0,0q�$ 0.5,0.5&�$��50��$��Dosis)�$��20(�$	��300��$��Center=�$��Center=�$��0q� .  ���	#FFE6E6E6"  FontAwesome.WPF.ImageAwesome  -
     Name$ 
loadingBar��   Icon$ Refresh=�   Spin . $��64��$��64��   
Foreground �	#FFE6E6E6�� -   $ 
logoHolder��$ HighQuality=�$	��128��$��0q�$	��128��$ 0.5,0.5&�$��logo.png��$ 0Q�� -   $ dataGrid��$y�	Collapsed=�$	��4,0q� � RowDefinitions �� $��64 ���  � ColumnDefinitions �� $
��404* ��� $
��389* �.+��� -   $ TopLeftLogo��$��10,5,0,5q�$��logo.png��$
��Left=�$��54��$ HighQuality=��� -    ��Name$ inQueue��$��4,0,4,50q�$��1�����	#FF313131
�������$��Dosis)���Z� �� -   $ allSongs��$��4,0,4,50q�$��1��$��1��
����������	#FF313131$��Dosis)���Z� r� -    r�Name$ mediaPlayer��$
��Left=�$��26��$��12,336.4,0,0q�$��1��$	��Top=�$	��382��$y�Hidden=�(  "System.Windows.Controls.WebBrowser  -     Name$ VideoPlayer��$y�	Collapsed=�$��4,0,4,50q�$��1���� $��Right=�$��0,0,4,4q�$
��Wrap=�$��Bottom=�$��Dosis)�$��20(�
�������$��Queue��$ 
0.491,0.53&�$��26���� $��0,0,4,4q�$
��Wrap=�$��Bottom=�$��Dosis)�$��20(�
�������$��Available Songs��$ 
0.491,0.53&�$��1��$��Right=�$	��120��$��26���� -   $
 
changeName��$��Change Name��$��1��$��0,0,4,10q�$��1��$��80��$��32��$��Dosis)�$��Bottom=�$��Right=����	#FF202020���	#FFFBC02D��Z� �� -    ��Name$ currentlyPlaying��$
��Left=�$��4,0,0,10q�$��1��$
��Wrap=�$��Currently Playing��$��Bottom=�$��32��$	��390��$��Dosis)�
��������� -   $
 
changeList��$��Change List��$��1��$��	0,0,94,10q�$��1��$��80��$��32��$��Dosis)�$��Bottom=�$��Right=����	#FF202020���	#FFFBC02D��Z� �� -   $
 	showVideo��$y�	Collapsed=�$��
Show Video��$��1��$��4,0,0,10q�$��1��$��32��$��Dosis)�$��Bottom=����	#FF202020���	#FFFBC02D$
��Left=�$��80����Z� 