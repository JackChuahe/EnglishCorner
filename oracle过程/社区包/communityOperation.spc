create or replace package communityOperation is

--��ȡָ����������Ϣ
       function getCommunityInfo(v_communityID in varchar2) return sys_refcursor;
 -- ��ȡָ�������Ķ���û�ͷ��
       function getCommunityUsersHeadImg(v_communityID in varchar2) return sys_refcursor;
       
  --- ����ָ����������ȡ��������������ж�̬���� �� ������ ������ʱ�� �����ߵ�ͷ��url
        function getCommunityAllContents(v_communityID in varchar2) return sys_refcursor;
  -- ͨ��contentID��ȡÿ��content���������ۼ����۵��˺��˵�ͷ��
       function GetContentComments(v_contentID in varchar2) return sys_refcursor;
       
   --- ͨ���ô���Ķ�ĳ��content��������Ϣ ���� contentID��userEmail������text, ������ 1 ���� 0
       function insertAComment(v_contentID in varchar2,v_userEmail in varchar2,v_text in varchar2) return varchar2;
       
  --- �����û�����Ķ�̬,����userEmail  communityID  text   picUrl ���в���
        function insertUserContent(v_userEmail in varchar2,v_communityID in varchar2,v_text in varchar2,v_picUrl in varchar2)return varchar2; 
end;
/
