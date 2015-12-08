create or replace package communityOperation is

--获取指定社区的信息
       function getCommunityInfo(v_communityID in varchar2) return sys_refcursor;
 -- 获取指定社区的多个用户头像
       function getCommunityUsersHeadImg(v_communityID in varchar2) return sys_refcursor;
       
  --- 传入指定社区，获取该社区发表的所有动态内容 和 发布者 及发布时间 发布者的头像url
        function getCommunityAllContents(v_communityID in varchar2) return sys_refcursor;
  -- 通过contentID获取每该content的所有评论及评论的人和人的头像
       function GetContentComments(v_contentID in varchar2) return sys_refcursor;
       
   --- 通过用传入的对某条content的评论信息 包括 contentID，userEmail，评论text, 并返回 1 或者 0
       function insertAComment(v_contentID in varchar2,v_userEmail in varchar2,v_text in varchar2) return varchar2;
       
  --- 插入用户发表的动态,传入userEmail  communityID  text   picUrl 进行插入
        function insertUserContent(v_userEmail in varchar2,v_communityID in varchar2,v_text in varchar2,v_picUrl in varchar2)return varchar2; 
end;
/
