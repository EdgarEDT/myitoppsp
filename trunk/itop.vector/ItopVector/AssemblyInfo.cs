using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;

//
// �йس��򼯵ĳ�����Ϣ��ͨ������
// ���Լ����Ƶġ�������Щ����ֵ���޸������
// ��������Ϣ��
//
[assembly: AssemblyTitle("ItopVector")]
[assembly: AssemblyDescription("ͼ�οؼ�")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("RabbitHome")]
[assembly: AssemblyProduct("ItopVector")]
[assembly: AssemblyCopyright("")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]		
[assembly :AllowPartiallyTrustedCallers()] 
//
// ���򼯵İ汾��Ϣ������ 4 ��ֵ���:
//
//      ���汾
//      �ΰ汾 
//      �ڲ��汾��
//      �޶���
//
// ������ָ��������Щֵ��Ҳ����ʹ�á��޶��š��͡��ڲ��汾�š���Ĭ��ֵ�������ǰ�
// ������ʾʹ�� '*':

[assembly: AssemblyVersion("3.0.1.*")]

//
// Ҫ�Գ��򼯽���ǩ��������ָ��Ҫʹ�õ���Կ���йس���ǩ���ĸ�����Ϣ����ο� 
// Microsoft .NET Framework �ĵ���
//
// ʹ����������Կ�������ǩ������Կ��
//
// ע��:
//   (*) ���δָ����Կ������򼯲��ᱻǩ����
//   (*) KeyName ��ָ�Ѿ���װ�ڼ�����ϵ�
//      ���ܷ����ṩ����(CSP)�е���Կ��KeyFile ��ָ����
//       ��Կ���ļ���
//   (*) ��� KeyFile �� KeyName ֵ����ָ������ 
//       �������д���:
//       (1) ����� CSP �п����ҵ� KeyName����ʹ�ø���Կ��
//       (2) ��� KeyName �����ڶ� KeyFile ���ڣ��� 
//           KeyFile �е���Կ��װ�� CSP �в���ʹ�ø���Կ��
//   (*) Ҫ���� KeyFile������ʹ�� sn.exe(ǿ����)ʵ�ù��ߡ�
//       ��ָ�� KeyFile ʱ��KeyFile ��λ��Ӧ�������
//       ��Ŀ���Ŀ¼����
//       %Project Directory%\obj\<configuration>�����磬��� KeyFile λ��
//       ����ĿĿ¼��Ӧ�� AssemblyKeyFile 
//       ����ָ��Ϊ [assembly: AssemblyKeyFile("..\\..\\mykey.snk")]
//   (*) ���ӳ�ǩ������һ���߼�ѡ�� - �й����ĸ�����Ϣ������� Microsoft .NET Framework
//       �ĵ���
//
[assembly: AssemblyDelaySign(false)]
//[assembly: AssemblyKeyFile("..\\..\\..\\tlvector.snk")]
[assembly: AssemblyKeyName("")]