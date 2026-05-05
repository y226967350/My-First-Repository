# 使用方法
## Unity2D景深摄像机使用方法
1.下载ParallaxCamera.cs和Parallaxlayer.cs
2.在unity中将ParallaxCamera组件添加到场景中的主摄像机中，创建一个玩家对象（如Player），将ParallaxCamera的Target设置为玩家对象，按照需求调整其他参数。
3.为场景中的远景，中景和近景对象添加Parallaxlayer组件，分别在Inspector面板中为其调整速度参数
 默认：远景 0.6，0.6 
      中景 0.3 0.3
      近景 0.1 0.1
