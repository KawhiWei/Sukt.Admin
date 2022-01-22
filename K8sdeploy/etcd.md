+ Ubuntu 安装etcd 
  ``` shell
  https://blog.csdn.net/IT_DREAM_ER/article/details/107470959

  # 设置版本为V3
  export ETCDCTL_API=3

  # 启用认证
   etcdctl auth enable

  # 查看角色列表
  etcdctl --endpoints http://127.0.0.1:2379 role list
  
  # 添加用户并设置密码
  etcdctl --endpoints http://127.0.0.1:2379 user add root

  #带有用户名密码的链接
  etcdctl --endpoints http://127.0.0.1:2379 --user=root:P@ssW0rd user get root

  ```